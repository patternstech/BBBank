import { Account } from "../../models/account";

export class AccountsListResponse {
    accounts: Account[];
    resultCount: number;
  }