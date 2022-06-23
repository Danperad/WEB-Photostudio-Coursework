import {ServiceModel} from "./Models";

declare type NewServiceModel = {
    service: ServiceModel,
    startTime: number | null,
    duration: number | null,
    hallId: number | null,
    employeeId: number | null,
    addressId: number | null,
    rentedItemId: number | null,
    number: number | null,
    isFullTime: boolean | null,
}

export default NewServiceModel;