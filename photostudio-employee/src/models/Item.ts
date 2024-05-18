enum ItemType {
  Simple = 1,
  Cloth,
  KidsCloth
}

interface Item {
  id: number,
  title: string,
  description: string,
  type: ItemType,
  costPerHour: number,
}

export type {Item}
export {ItemType}
