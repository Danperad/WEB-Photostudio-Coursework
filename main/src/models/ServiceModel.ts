enum ServiceType {
    Simple = 1,
    HallRent,
    Photo,
    ItemRent,
    Style
}

declare type ServiceModel = {
    id: number,
    title: string,
    description: string,
    cost: number,
    type: ServiceType,
    photos: string[]
}

export default ServiceModel;
export {ServiceType};