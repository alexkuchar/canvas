import { HttpErrorResponse } from '@angular/common/http';

interface ApiErrorResponse {
  error?: {
    code?: string;
    message?: string;
  };
  timestamp?: string;
}

/**
 * Extracts the error message from an HttpErrorResponse.
 * Handles the API's error response structure: { error: { code, message }, timestamp }
 */
export function getErrorMessage(error: HttpErrorResponse): string {
  if (typeof error.error === 'string') {
    return error.error;
  }

  const apiError = error.error as ApiErrorResponse;
  return apiError?.error?.message ?? error.message ?? 'An unexpected error occurred';
}
