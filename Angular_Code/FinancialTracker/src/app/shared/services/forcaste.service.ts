import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Holidays } from '../models/holidays.model';
import { Observable } from 'rxjs';
import { Resource } from '../models/resource.model';

@Injectable({
  providedIn: 'root'
})
export class ForcastServiceService {

 

  constructor(private http: HttpClient) { }

  getHolidayList():Observable<any>{
    return this.http.get<any[]>(environment.holidaysURL);
  }
  postRecords(data:Resource[]):Observable<Resource[]>{
    console.log(data);
    return this.http.post<Resource[]>(environment.apiURL,data);
  }
}
