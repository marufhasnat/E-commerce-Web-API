﻿using E_commerce_Web_API.DTOs;

namespace E_commerce_Web_API.Controllers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode {  get; set; }
        public DateTime TimeStamp { get; set; }

        // Constructor for successful response
        private ApiResponse(bool success, string message, T? data, List<string>? errors, int statusCode) { 
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
            StatusCode = statusCode;
            TimeStamp = DateTime.UtcNow;
        }

        // static method for creating a successful response
        public static ApiResponse<T> SuccessResponse(T? data, int statusCode, string message = "")
        {
            return new ApiResponse<T>(true, message, data, null, statusCode);
        }

        // static method for creating an error response
        public static ApiResponse<T> ErrorResponse(List<string> errors, int statusCode, string message = "")
        {
            return new ApiResponse<T>(false, message, default(T), errors, statusCode);
        }
    }
}
