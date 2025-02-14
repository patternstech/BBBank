export interface ApiResponse<T> {
    statusCode: number;
    message: string;
    result?: { 
      message: string;
      data: T;
    };
    isError: boolean;
    responseException?: { 
      exceptionMessage: string;
    };
    errorMessage?: string;
  }