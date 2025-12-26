export interface Profile {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}

export interface TokenPair {
  accessToken: string;
  refreshToken: string;
  refreshTokenExpiresAt: Date;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  user: Profile;
  tokens: TokenPair;
}

export interface User {
  user: Profile;
  tokens: TokenPair;
}

export interface RefreshRequest {
  refreshToken: string;
}

export interface RefreshResponse {
  accessToken: string;
  refreshToken: string;
  refreshTokenExpiresAt: Date;
}
