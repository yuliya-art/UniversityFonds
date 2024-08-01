export class ClassRoomRequest {
    constructor(
        public name: string,
        public buildingId: string,
        public capacity: number,
        public number: number,
        public typeId: number,
        public floor: number
    ) { }
 }