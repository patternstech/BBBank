

export interface AccountByUserInfo extends AccountInfo {

}
export interface AccountInfo {
    accountNumber: string;
    accountTitle: string;
    currentBalance: number;
    accountStatus: string;
    userImageUrl: string;
}

