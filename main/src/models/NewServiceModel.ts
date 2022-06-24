import {Employee, Hall, RentedItem, Service} from "./Models";

declare type NewServiceModel = {
    id: number
    service: Service,
    startTime: number | null,
    duration: number | null,
    hall: Hall | null,
    employee: Employee | null,
    address: string | null,
    rentedItem: RentedItem | null,
    number: number | null,
    isFullTime: boolean | null,
}

export default NewServiceModel;