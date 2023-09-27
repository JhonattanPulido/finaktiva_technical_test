import { ToastrService } from 'ngx-toastr';
import { Log } from 'src/app/models/log.model';
import { NgxSpinnerService } from "ngx-spinner";
import { LogType } from 'src/app/enums/log.enum';
import { Status } from 'src/app/models/status.model';
import { LogsService } from 'src/app/services/logs.service';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-log',
  templateUrl: './create-log.component.html',
  styleUrls: ['./create-log.component.css']
})
export class CreateLogComponent {

  public selectedStatus: Status; // Status filter selected value
  public statusList: Status[]; // Status list
  public logForm: FormGroup; // Log form group
  @ViewChild('logTypes') logTypes!: ElementRef;

  constructor(
    private toastr: ToastrService,
    private formBuilder: FormBuilder,
    private logsService: LogsService,
    private spinner: NgxSpinnerService
  ) {
    this.statusList = this.getStatusList();
    this.selectedStatus = this.statusList.filter((status) => status.id == LogType.Success)[0];
    this.logForm = this.formBuilder.group({
      creationDate: [null, [Validators.pattern('^[0-9]{2}\/[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}$')]],
      type: [this.selectedStatus.id, [Validators.required]],
      description: [null, [Validators.required, Validators.minLength(4), Validators.maxLength(128)]]
    });
  }

  /**
   * Create event log
   */
  public onSubmit(): void {
    this.spinner.show();

    // Validate if form is correct
    if (!this.logForm.valid) {
      this.toastr.error('The form is not successfully filled', 'Form Error', {
        closeButton: true
      });
    }

    // Set log data
    const log: Log = {
      description: this.logForm.controls['description'].value,
      type: this.logForm.controls['type'].value
    };

    // Get creation date value
    var creationDate: string = this.logForm.controls['creationDate'].value;

    if (creationDate) {
      const dateParts: string[] = creationDate.split('T');
      log.creationDate = new Date(`2023/${dateParts[0]}`);
      const dateHour: string[] = dateParts[1].split(':');

      const hours: number = Number.parseInt(dateHour[0]);
      const minutes: number = Number.parseInt(dateHour[1]);
      const seconds: number = Number.parseInt(dateHour[2]);
      log.creationDate.setHours(hours, minutes, seconds);
    }

    this.toastr.success('Even log created', 'Success', {
      closeButton: true
    });

    this.spinner.hide();
  }

  /**
   * Set selected log type
   * @param selected Selected status
   */
  public setSelectedLogType(selected: Status): void {
    this.selectedStatus = selected;
    this.manageLogTypesMenu(false); // Close log types menu
    this.logForm.patchValue({ // Path type form field
      type: selected.id
    });
  }

  /**
   * Open or close log types menu
   * @param open Open flag
   */
  public manageLogTypesMenu(open: boolean): void {
    this.logTypes.nativeElement.style.display = open ? 'flex' : 'none';
  }

  /**
   * Get status list
   * @returns Prepared status list
   */
  private getStatusList(): Status[] {
    return [      
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
