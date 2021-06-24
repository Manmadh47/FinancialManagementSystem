import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ManageListService } from '../shared/services/manage-list.service';
import { Resource } from '../shared/models/resource.model';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../shared/confirm_dialog/confirm-dialog.component';
import { SummaryService } from '../shared/services/summary.service';

@Component({
  selector: 'app-manage-list-page',
  templateUrl: './manage-list-page.component.html',
  styleUrls: ['./manage-list-page.component.scss']
})
export class ManageListPageComponent implements OnInit {

  tableFormArray:FormArray
   resourceData:Resource[];
   monthValues: any[];
   yearValues:any[];
   projectValues: any[];
   tempYear:any[]=[];
   tempMonth:any[]=[];
   filteredRows:any[]=[];
   readonly: boolean;
   buttonText:string;
   selectedColor:string;
   toolTip:string;
   disable: boolean;
   rowsSelects: number[] = [];
   name=null;
   selectedYear:string;
   selectedMonth:string;
   selectedProject:string;
   latestMonth:string[];
   dropdownValues:any[]=[];

  displayedColumns: string[] = ['UID', 'EmployeeName','Track','Location','City','EngagementType',
                  'EmployeeType','BillRate','AvailableHours','NBHours','NBCategory','BilledHours','TotalRevenue',
                  'CostRate','CostHours','TotalExpenses','Month','Year','StartDate','EndDate','Remarks','star'];

  constructor(private listService:ManageListService,private formBuilder: FormBuilder,
              public summaryService:SummaryService,private dialog:MatDialog) { }

  getFormGroup(index: number) {
    return this.tableFormArray.at(index) as FormGroup;
  }

  ngOnInit(): void {
    this.currentDateRecords();
    this.getAllYears();
    this.controls(); 
  }
  controls(){
    this.disable=true;
    this.readonly = true;
    this.buttonText="edit";
    this.selectedColor="primary";
    this.toolTip="Edit Records";
  }

  getAllYears(){
    this.summaryService.getYears().subscribe((res:any[])=>
    this.yearValues=res);
  }
  getResources(){
    this.summaryService.getYears().subscribe((res:any[])=>
    this.yearValues=res);
  }
  currentDateRecords(){
    this.summaryService.getByCurrentDate().subscribe((res:any[])=>{
      this.dropdownValues=res;
      this.tableFormArray=this.formBuilder.array(res.map(resource => this.setUsersFormArray(resource)));
      this.rowsSelects= this.rowsSelects = this.tableFormArray.controls.map((x, index) => index);

      this.latestMonth=[...new Set(res.map(item => item.month.toUpperCase()))];
    });
  }

  private setUsersFormArray(resource: any){
    return this.formBuilder.group({
      id:[resource.id],
      project_id:[resource.project_id],
      location:[resource.location],
      city:[resource.city],
      track:[resource.track],
      uid:[resource.uid],
      employee_name:[resource.employee_name], 
      engagement_type:[resource.engagement_type],
      employee_type:[resource.employee_type],
      available_hours:[resource.available_hours],
      nb_hours:[resource.nb_hours],
      bill_rate:[resource.bill_rate],
      nb_category:[resource.nb_category],
      billed_hours:[resource.billed_hours],
      total_revenue:[resource.total_revenue],
      cost_rate:[resource.cost_rate],
      cost_hours:[resource.cost_hours],
      total_expenses:[resource.total_expenses],
      month:[resource.month],
      year:[resource.year],
      start_date:[resource.start_date],
      end_date:[resource.end_date],
      remarks:[resource.remarks]
    });
  }
 
  onChangeYear(event){
    this.monthValues=[];
    this.summaryService.getMonths(event).subscribe((months:any[])=>this.monthValues=months);
    this.selectedYear=event;
  }

  onChangeMonth(event){
    this.projectValues=[];
    this.summaryService.getRecords(this.selectedYear,event).subscribe((res:any[])=>{
      this.tempMonth=res;
      this.selectedMonth=event;
      this.projectValues=[...new Set(this.tempMonth.map(item => item.project_id))];
    })
  }

  onChangeProject(event){
    this.latestMonth=[]
    for(let row in this.tempMonth){
      if(this.tempMonth[row].project_id==event){
        this.filteredRows.push(this.tempMonth[row]);
      }
    }
     this.tableFormArray=this.formBuilder.array(this.filteredRows.map(resource => this.setUsersFormArray(resource)));
    this.dropdownValues=this.filteredRows;
    this.rowsSelects = this.tableFormArray.controls.map((x, index) => index);
    this.filteredRows=[];
    this.selectedProject=event;
  }

  toggleEdit(): void {
    this.readonly = !this.readonly;
    if(this.buttonText === 'edit') { 
      this.buttonText = 'cancel';
      this.selectedColor='warn';
      this.toolTip="Cancel";
    } else {
      this.buttonText = 'edit';
      this.selectedColor='primary';
      this.toolTip="Edit Records";
    }
    this.disable = !this.disable;
  }

  selectName(name: string|null) {
    if (name) {
      this.rowsSelects = this.tableFormArray.controls
        .map((x, index) => ({
          index: index,
          select: x.value.employee_name == name
        }))
        .filter(x => x.select)
        .map(x => x.index);
    } else {
      this.rowsSelects = this.tableFormArray.controls.map((x, index) => index);
    }
  }
  selectTrack(name: string|null) {
    if (name) {
      this.rowsSelects = this.tableFormArray.controls
        .map((x, index) => ({
          index: index,
          select: x.value.track == name
        }))
        .filter(x => x.select)
        .map(x => x.index);
    } else {
      this.rowsSelects = this.tableFormArray.controls.map((x, index) => index);
    }
  }

  updateData(){
    this.listService.touchedRows = this.tableFormArray.controls.filter(row => row.touched).map(row => row.value);
    console.log(this.listService.touchedRows);
  }
  confirmDialog(){
    this.updateData();
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      disableClose:true,
      width:"20%",
      position:{'top':'0'}
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getAllYears();
      this.onChangeYear(this.selectedYear);
      this.onChangeMonth(this.selectedMonth);
      this.onChangeProject(this.selectedProject);
      this.controls();
    });
  }
}
