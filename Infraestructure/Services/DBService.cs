using Core.Enumerations;
using Core.Intefaces;
using Core.Models;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructure.Services
{
    public class DBService : IDBService
    {
        private readonly ResponseDB _responseBD;
        private readonly ILogService _logService;
        public DBService(ILogService logService)
        {
            _responseBD = new ResponseDB();
            _logService = logService;
        }
        public async Task<ResponseDB> CallSP(string cs, string sp, Dictionary<string, dynamic> parameters)
        {
            using SqlConnection sql = new(cs);
            try
            {
                using SqlCommand cmd = new(sp, sql)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (KeyValuePair<string, dynamic> item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                await sql.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

                _responseBD.Code = ResponseCode.Success;
                return _responseBD;
            }
            catch (SqlException ex)
            {
                _responseBD.Code = ResponseCode.Error;
                _responseBD.Description = ex.Message;

                _logService.SaveLogApp($"{nameof(CallSP)}{nameof(SqlException)} - {sp} - {ex.Message}", LogType.Information);
                return _responseBD;
            }
            catch (TimeoutException ex)
            {
                _responseBD.Code = ResponseCode.Timeout;
                _responseBD.Description = ex.Message;
                _logService.SaveLogApp($"{nameof(CallSP)}{nameof(TimeoutException)} - {sp} - {ex.Message} - {sp}", LogType.Information);

                return _responseBD;
            }
            catch (Exception ex)
            {
                _responseBD.Code = ResponseCode.FatalError;
                _responseBD.Description = ex.Message;
                _logService.SaveLogApp($"{nameof(CallSP)}{nameof(Exception)} - {sp} - {ex.Message} | {ex.StackTrace}", LogType.Error);
                return _responseBD;
            }
            finally
            {
                await sql.CloseAsync();
            }

        }
        public async Task<ResponseDB> CallSPData(string cs, string sp, Dictionary<string, dynamic> parameters)
        {
            DataTable dt = new();
            using SqlConnection sql = new(cs);
            try
            {
                using SqlCommand cmd = new(sp, sql)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (KeyValuePair<string, dynamic> item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                await sql.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader); sql.Close(); reader.Close(); cmd.Dispose(); sql.Dispose();

                _responseBD.Code = ResponseCode.Success;
                _responseBD.Data = dt;
                return _responseBD;
            }
            catch (SqlException ex)
            {
                _responseBD.Code = ResponseCode.Error;
                _responseBD.Description = ex.Message;
                _logService.SaveLogApp($"{nameof(CallSPData)}{nameof(SqlException)} - {sp} - {ex.Message}", LogType.Information);
                return _responseBD;
            }
            catch (TimeoutException ex)
            {
                _responseBD.Code = ResponseCode.Timeout;
                _responseBD.Description = ex.Message;
                _logService.SaveLogApp($"{nameof(CallSPData)}{nameof(TimeoutException)} - {sp} - {ex.Message}", LogType.Information);
                return _responseBD;
            }
            catch (Exception ex)
            {
                _responseBD.Code = ResponseCode.FatalError;
                _responseBD.Description = ex.Message;
                _logService.SaveLogApp($"{nameof(CallSPData)}{nameof(Exception)} - {sp} - {ex.Message} | {ex.StackTrace}", LogType.Error);
                return _responseBD;
            }
            finally
            {
                await sql.CloseAsync();
            }

        }
    }
}
