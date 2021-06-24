import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ExportExcelComponent } from './ExportDocuments/export-excel/export-excel.component';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { ConfirmDialogComponent } from '../app/shared/confirm_dialog/confirm-dialog.component';
import { SummaryComponent } from '../app/summary/summary-page/summary.component';
import { ViewSummaryDialogComponent } from "./ExportDocuments/summary_dialog/view-summary-dialog.component";
import { GenerateForecastComponent } from './forecast/generate-forecast.component';
import { ManageListPageComponent } from './manage-list/manage-list-page.component';
import { ChartModule } from 'angular-highcharts';

@NgModule({
  declarations: [
    AppComponent,
    ExportExcelComponent,
    SummaryComponent,
    ConfirmDialogComponent,
    ViewSummaryDialogComponent,
    GenerateForecastComponent,
    ManageListPageComponent    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    HomeModule,
    ChartModule
  ],
  bootstrap: [AppComponent],
  entryComponents:[SummaryComponent,ConfirmDialogComponent]
})
export class AppModule { }
