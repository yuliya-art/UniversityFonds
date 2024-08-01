import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { ClassRoom } from "../models/class-room.model";
import { map, Observable } from "rxjs";
import { ClassRoomRequest } from "../models/class-room-request.model";
import { ClassRoomBuilding } from "../models/class-room-building.model";
import { ClassRoomType } from "../models/class-room-type.model";

@Injectable({
  providedIn: "root",
})
export class ClassRoomService {
  private apiUrl = environment.classRoomApiUrl;

  constructor(private http: HttpClient) {}

  getClassRooms(): Observable<ClassRoom[]> {
    return this.http
      .get<
        {
          id: string;
          name: string;
          building: string;
          buildingId: string;
          capacity: number;
          number: number;
          type: string;
          typeId: number;
          floor: number;
        }[]
      >(`${this.apiUrl}/ClassRoom`)
      .pipe(
        map((data) =>
          data.map(
            (item) =>
              new ClassRoom(
                item.id,
                item.name,
                item.building,
                item.buildingId,
                item.capacity,
                item.number,
                item.type,
                item.typeId,
                item.floor
              )
          )
        )
      );
  }

  getClassRoomById(id: string): Observable<ClassRoom> {
    return this.http
      .get<{
        id: string;
        name: string;
        building: string;
        buildingId: string;
        capacity: number;
        number: number;
        type: string;
        typeId: number;
        floor: number;
      }>(`${this.apiUrl}/ClassRoom/${id}`)
      .pipe(
        map(
          (item) =>
            new ClassRoom(
              item.id,
              item.name,
              item.building,
              item.buildingId,
              item.capacity,
              item.number,
              item.type,
              item.typeId,
              item.floor
            )
        )
      );
  }

  createClassRoom(classRoom: ClassRoomRequest): Observable<ClassRoom> {
    return this.http
      .post<{
        id: string;
        name: string;
        building: string;
        buildingId: string;
        capacity: number;
        number: number;
        type: string;
        typeId: number;
        floor: number;
      }>(`${this.apiUrl}/ClassRoom`, classRoom)
      .pipe(
        map(
          (item) =>
            new ClassRoom(
              item.id,
              item.name,
              item.building,
              item.buildingId,
              item.capacity,
              item.number,
              item.type,
              item.typeId,
              item.floor
            )
        )
      );
  }

  getBuildings(): Observable<ClassRoomBuilding[]> {
    return this.http
      .get<{ id: string; name: string }[]>(`${this.apiUrl}/Buildings`)
      .pipe(
        map((data) =>
          data.map((item) => new ClassRoomBuilding(item.id, item.name))
        )
      );
  }
  getClassRoomTypes(): Observable<ClassRoomType[]> {
    return this.http
      .get<{ id: number; description: string }[]>(
        `${this.apiUrl}/ClassRoomTypes`
      )
      .pipe(
        map((data) =>
          data.map((item) => new ClassRoomType(item.id, item.description))
        )
      );
  }

  updateClassRoom(id: string, request: ClassRoomRequest) {
    return this.http
      .put<{
        id: string;
        name: string;
        building: string;
        buildingId: string;
        capacity: number;
        number: number;
        type: string;
        typeId: number;
        floor: number;
      }>(`${this.apiUrl}/ClassRoom/${id}`, request)
      .pipe(
        map(
          (item) =>
            new ClassRoom(
              item.id,
              item.name,
              item.building,
              item.buildingId,
              item.capacity,
              item.number,
              item.type,
              item.typeId,
              item.floor
            )
        )
      );
  }

  removeClassRoom(id: string): Observable<Object> {
    return this.http.delete(`${this.apiUrl}/ClassRoom/${id}`);
  }
}
