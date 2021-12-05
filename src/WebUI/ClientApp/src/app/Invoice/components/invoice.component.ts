import { Component, OnInit, QueryList, ViewChild, ViewChildren } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatTable, MatTableDataSource } from "@angular/material/table";
import { InvoicesClient, CreateInvoiceCommand, UpdateInvoiceCommand, InvoiceDto } from '../../web-api-client';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EnterDivDirective } from "../directives/enter-div.directive";
import { KeyBoardService } from "../services/keyboard.service";

@Component({
  selector: 'app-invoice-component',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss']
})

export class InvoiceComponent implements OnInit {
  departments: string[] = ["All", "HR", "IT", "Accounting"];

  displayedColumns: string[] = ['id', 'invoiceNumber', 'amount', 'department', 'deleteInvoice'];
  dataSource: MatTableDataSource<InvoiceDto>

  @ViewChild(MatTable) table: MatTable<InvoiceDto>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChildren(EnterDivDirective) inputs: QueryList<EnterDivDirective>

  totalRows = 0;
  pageSize = 5;
  pageNumber = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  firstLoad: Boolean;
  NoinvalidInvoicesExist: boolean = true;

  constructor(private invoicesClient: InvoicesClient, private matSnackBar: MatSnackBar, private keyboardService: KeyBoardService) {
    this.loadTodoItems();
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.dataSource.paginator = this.paginator;

    this.keyboardService.keyBoard.subscribe(res => {
      this.moveToNextRow(res)
    });
  }

  moveToNextRow(object: any) {

    const inputToArray = this.inputs.toArray()
    let index = inputToArray.findIndex(x => x.element === object.element)

    let currentInvoice = object.invoice;
    this.saveInvoice(currentInvoice);

    index++;

    if (index > 0 && index < this.inputs.length) {
      inputToArray[index].element.nativeElement.focus();
    }
  }

  loadTodoItems(): void {
    this.invoicesClient.getTodoItemsWithPagination(this.pageNumber + 1, this.pageSize).subscribe(
      result => {
        this.dataSource.data = result.items;
        this.paginator.pageIndex = this.pageNumber;
        this.paginator.length = result.totalCount;
        this.paginator.pageSize = this.pageSize;
        this.totalRows = result.totalCount;

        if (result.items.length == 0) {
          this.addEmptyRow();
        }
      },
      error => console.error(error)
    );
  }

  deleteInvoice(rowIndex: number, invoiceId: number): void {
    this.invoicesClient.delete(invoiceId)
      .subscribe(
        () => {
          this.dataSource.data = this.dataSource.data.slice(rowIndex, 1);
          this.table.renderRows();
        }
      );
  }

  saveInvoice(invoice: InvoiceDto): void {
    if (invoice.id == 0 || invoice.id == null) {
      this.invoicesClient.create(CreateInvoiceCommand.fromJS(invoice))
        .subscribe(
          result => {
            invoice.id = result.id;
            this.addEmptyRow();
          },
          error => console.error(error)
        );
    } else {
      this.invoicesClient.update(invoice.id, UpdateInvoiceCommand.fromJS(invoice))
        .subscribe(
          () => console.log('Update succeeded.'),
          error => console.error(error)
        );
    }
  }

  validateInvoices(): void {
    this.invoicesClient.gatValidationErrors()
      .subscribe(result => {
        if (result) {
          this.matSnackBar.open("One or more invoices are invalid.", "Close", { verticalPosition: 'top', horizontalPosition: 'center' });
        }
      },
        error => console.error(error)
      );
  }

  onPageChanged(event: PageEvent): void {
    this.pageNumber = event.pageIndex;
    this.pageSize = event.pageSize;
    this.firstLoad = false;
    this.loadTodoItems();
  }

  addEmptyRow(): void {
    this.dataSource.data.push(new InvoiceDto());
    this.table.renderRows();
  }
}
