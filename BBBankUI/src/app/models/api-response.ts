export interface ApiResponse<T = undefined> {
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