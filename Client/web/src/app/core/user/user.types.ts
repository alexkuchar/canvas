export interface UpdateUserRequest {
  id: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  password?: string;
}

export interface UpdateUserResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}
