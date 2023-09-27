import { NgxSpinnerService } from "ngx-spinner";
import { LogsService } from 'src/app/services/logs.service';
import { Pagination, PaginationOutput } from 'src/app/models/pagination.model';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';


@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.css']
})
export class LogsComponent implements OnInit, AfterViewInit {

  public pagination: Pagination; // Pagination data
  public data?: PaginationOutput; // Logs data
  @ViewChild('statusFilter') statusFilter!: ElementRef;

  constructor(
    private logsService: LogsService,
    private spinner: NgxSpinnerService
    )
  {
    // Get and validate pagination information
    var paginationJson: string | null = sessionStorage.getItem('pagination');
    this.pagination = paginationJson ? JSON.parse(paginationJson, function (key: string, value: any) {
      if (key == 'initialDate' || key == 'finalDate') return new Date(value);
      return value;
    }) : this.setPagination();
    sessionStorage.setItem('pagination', JSON.stringify(this.pagination));
  }

  ngOnInit(): void {
    this.spinner.show();
    // Get paginated logs
    this.logsService.getAll(this.pagination)
      .subscribe({
        next: (data) => {
          this.data = data;
        }
      });
  }

  ngAfterViewInit(): void {
    this.spinner.hide();
  }

  /**
   * Open or close status filter menu
   * @param open Open flag
   */
  public manageStatusFilter(open: boolean): void {
    console.log(this.statusFilter);
    this.statusFilter.nativeElement.style.display = open ? 'flex' : 'none';
  }

  /**
   * Get pagination data
   * @returns Prepared pagination data
   */
  private setPagination(): Pagination {
    // Get actual date and set time at 12 AM
    const actualDate: Date = new Date();
    // Get 15 days ago date and set time at 12 AM
    const beforeDate: Date = new Date();
    beforeDate.setDate(beforeDate.getDate() - 15);

    // Return prepared pagination data
    return {
      pageNumber: 1,
      pageSize: 15,
      initialDate: beforeDate,
      finalDate: actualDate
    }
  }
}
