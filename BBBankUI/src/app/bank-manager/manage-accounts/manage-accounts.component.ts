import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Account } from '../../models/account';
import { environment } from '../../../environments/environment.development';
import { AccountsService } from '../services/accounts.service';
import { AccountsListResponse } from '../models/accounts-list-response';
import { ApiResponse } from '../../models/api-response';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { tap } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manage-accounts',
  imports: [MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    CurrencyPipe,
    CommonModule],
  templateUrl: './manage-accounts.component.html',
  styleUrl: './manage-accounts.component.css'
})
export class ManageAccountsComponent implements OnInit {
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  dataSource: MatTableDataSource<Account>;
  accounts: Account[];
  displayedColumns: string[] = [
    'accountTitle',
    'accountNumber',
    'currentBalance',
    'email',
    'phoneNumber',
    'accountStatus',
    'button'
  ];
  resultCount: number;
  pageSize = environment.gridDefaultPageSize;

  constructor(private accountService: AccountsService, private router: Router) { }
  ngOnInit() {
    this.paginator.page
    .pipe(        
      tap(() => this.loadGridData(this.paginator.pageIndex, this.paginator.pageSize))
    )
    .subscribe();
    this.loadGridData(0, environment.gridDefaultPageSize);
  }
  loadGridData(pageIndex: number, pageSize: number) {
    this.accountService.getAllAccountsPaginated(pageIndex, pageSize)
      .subscribe({
        next: (res: ApiResponse<AccountsListResponse>) => {
          this.dataSource = new MatTableDataSource(res.result.data.accounts);
          this.accounts = res.result.data.accounts;
          this.resultCount = res.result.data.resultCount;
          this.dataSource.sort = this.sort;
        },
        error: (error) => {
          console.log(error);

        },
      });
  }
  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    const filteredData = this.accounts.filter(account =>
      Object.entries({
        accountTitle: account.accountTitle,
        currentBalance: account.currentBalance?.toString(),
        email: account.user?.email,
        phoneNumber: account.user?.phoneNumber,
        accountNumber: account.accountNumber
      }).some(([_, value]) =>
        value?.toString().toLowerCase().includes(filterValue)
      )
    );
    this.dataSource = new MatTableDataSource(filteredData);
  }
  Update(account: Account) {
    this.router.navigate(['bank-manager/create-account'], { state: { data: account } });
  }

}
