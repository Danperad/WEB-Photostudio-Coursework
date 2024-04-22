import {Employee, Hall, RentedItem, Service} from "./Models";

declare type NewServiceModel = {
    id: number
    service: Service,
    startDateTime: Date | null,
    duration: number | null,
    hall: Hall | null,
    employee: Employee | null,
    rentedItem: RentedItem | null,
    number: number | null,
    isFullTime: boolean | null,
}

export default NewServiceModel;