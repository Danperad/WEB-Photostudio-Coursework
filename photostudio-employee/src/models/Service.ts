enum ServiceType {
  Simple = 1,
  HallRent,
  Photo,
  ItemRent,
  Style
}

interface Service {
  id: number,
  title: string,
  description: string,
  type: ServiceType,
  cost: number
}

export {ServiceType}
export type {Service}
