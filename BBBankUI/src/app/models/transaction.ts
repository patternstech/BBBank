export interface Transaction {
    id: string;
    transactionType: TransactionType;
    transactionDate: Date;
    transactionAmount: number;
  }
  export enum TransactionType {
    Deposit = 0,
    Withdraw = 1,
  }