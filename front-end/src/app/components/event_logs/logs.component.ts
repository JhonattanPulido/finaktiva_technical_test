import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { LogType } from "src/app/enums/log.enum";
import { Status } from "src/app/models/status.model";
import { LogsService } from 'src/app/services/logs.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Pagination, PaginationOutput } from 'src/app/models/pagination.model';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';


@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.css']
})
export class LogsComponent implements OnInit, AfterViewInit {

  public statusSelectedFilter: Status; // Status filter selected value
  public statusList: Status[]; // Status list
  public filtersForm: FormGroup; // Filters form
  public pagination: Pagination; // Pagination data
  public data?: PaginationOutput; // Logs data
  @ViewChild('statusFilter') statusFilter!: ElementRef;

  constructor(
    private toastr: ToastrService,
    private formBuilder: FormBuilder,
    private logsService: LogsService,
    private spinner: NgxSpinnerService
    )
  {
    this.spinner.show();
    // Set status list
    this.statusList = this.getStatusList();

    // Get and validate pagination information
    var paginationJson: string | null = sessionStorage.getItem('pagination');
    this.pagination = paginationJson ? JSON.parse(paginationJson, function (key: string, value: any) {
      if (key == 'initialDate' || key == 'finalDate') return new Date(value);
      return value;
    }) : this.setPagination();
    sessionStorage.setItem('pagination', JSON.stringify(this.pagination));

    // Set initial status selected filter
    this.statusSelectedFilter = this.statusList.filter((status) => status.id == this.pagination.type)[0];

    // Set filters form group
    this.filtersForm = this.formBuilder.group({
      initialDate: [this.getMonthAndDay(this.pagination.initialDate), [Validators.required, Validators.pattern('^[0-9]{2}\/[0-9]{2}$')]],
      finalDate: [this.getMonthAndDay(this.pagination.finalDate), [Validators.required, Validators.pattern('^[0-9]{2}\/[0-9]{2}$')]],
      type: [this.pagination.type, Validators.required]
    });
  }

  ngOnInit(): void {
    this.getAll();
  }

  ngAfterViewInit(): void {
    this.spinner.hide();
  }

  /**
   * Filter logs
   */
  public onFilterSubmit(): void {
    this.spinner.show();

    // Validate if form is correct
    if (!this.filtersForm.valid) {
      this.toastr.error('The form is not successfully filled', 'Form Error', {
        closeButton: true
      });
    }

    // Get form values
    this.pagination.initialDate = new Date(`2023/${this.filtersForm.controls['initialDate'].value}`);
    this.pagination.finalDate = new Date(`2023/${this.filtersForm.controls['finalDate'].value}`);
    this.pagination.type = this.filtersForm.controls['type'].value;

    // Save selected values in session storage
    sessionStorage.setItem('pagination', JSON.stringify(this.pagination));

    // Get paginated logs
    this.getAll();
  }

  private getAll(): void {
    // Get paginated logs
    this.logsService.getAll(this.pagination)
      .subscribe({
        next: (data) => {
          this.data = data;
          this.spinner.hide();
        }
      });
  }

  /**
   * Set status selected filter
   * @param selected Selected status
   */
  public setStatusSelectedFilter(selected: Status): void {
    this.statusSelectedFilter = selected;
    this.manageStatusFilter(false); // Close status filter menu
    this.filtersForm.patchValue({ // Path type form field
      type: selected.id
    });
  }

  /**
   * Open or close status filter menu
   * @param open Open flag
   */
  public manageStatusFilter(open: boolean): void {
    this.statusFilter.nativeElement.style.display = open ? 'flex' : 'none';
  }

  /**
   * Get day and month from date
   * @param date Date to process
   */
  private getMonthAndDay(date: Date): string {
    const day: number = date.getDate();
    const month: number = date.getMonth() + 1;
    return `${month >= 10 ? month : `0${month}`}/${day >= 10 ? day : `0${day}`}`;
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
      type: LogType.All,
      initialDate: beforeDate,
      finalDate: actualDate
    }
  }

  /**
   * Get status list
   * @returns Prepared status list
   */
  private getStatusList(): Status[] {
    return [
      {
        id: LogType.All,
        name: 'All',
        color: '000000'
      },
      {
        id: LogType.Success,
        name: 'Success',
        color: '2ecc71'
      },
      {
        id: LogType.Info,
        name: 'Info',
        color: '3498db'
      },
      {
        id: LogType.Warning,
        name: 'Warning',
        color: 'e67e22'
      }
    ]
  }
}
