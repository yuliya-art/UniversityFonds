import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Building } from '../models/building.model';
import { environment } from '../../environments/environment';
import { BuildingRequest } from '../models/update-building-request.model';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  private apiUrl = environment.buildingApiUrl;

  constructor(private http: HttpClient) { }

  getBuildings(): Observable<Building[]> {
    return this.http.get<{id: string, name: string, description: string, address: string, floorsNumber: number}[]>(`${this.apiUrl}/Buildings`).pipe(
      map(data => data.map(item => new Building(item.id, item.name, item.description, item.address, item.floorsNumber)))
    );
  }

  getBuildingById(id: string): Observable<Building> {
    return this.http.get<{id: string, name: string, description: string, address: string, floorsNumber: number}>(`${this.apiUrl}/Buildings/${id}`).pipe(
      map(data => new Building(data.id, data.name, data.description, data.address, data.floorsNumber))
    );
  }

  updateBuilding(id: string, building: BuildingRequest): Observable<Building> {
     return this.http.put<{id: string, name: string, description: string, address: string, floorsNumber: number}>(`${this.apiUrl}/Buildings/${id}`, building).pipe(
      map(data => new Building(data.id, data.name, data.description, data.address, data.floorsNumber))
    );
  }

  createBuilding(building: BuildingRequest): Observable<Building> {
    return this.http.post<{id: string, name: string, description: string, address: string, floorsNumber: number}>(`${this.apiUrl}/Buildings`, building).pipe(
     map(data => new Building(data.id, data.name, data.description, data.address, data.floorsNumber))
   );
 }

  removeBuilding(id: string): Observable<Object> {
    return this.http.delete(`${this.apiUrl}/Buildings/${id}`);
 }
}