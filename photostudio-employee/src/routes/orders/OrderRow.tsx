import {Client, Order} from "@models/*";
import {TableCell, TableRow} from "@mui/material";
import dayjs from "dayjs";

type OrderRowProps = {
  order: Order,
  onOrderSelect: () => void
}

function OrderRow(props: OrderRowProps) {
  const {order, onOrderSelect} = props
  const client = order.client as Client

  const getClientFullName = () => {
    let name = client.lastName + ` ` + client.firstName.charAt(0) + `.`
    if (client.middleName)
      name += ` ` + client.middleName.charAt(0) + `.`
    return name
  }

  return (
    <TableRow sx={{cursor: "pointer"}} onClick={() => {onOrderSelect()}}>
      <TableCell>{order.number}</TableCell>
      <TableCell>{getClientFullName()} </TableCell>
      <TableCell>{dayjs(order.dateTime).format(`DD-MM-YYYY HH:mm`)}</TableCell>
      <TableCell>{order.totalPrice}</TableCell>
      <TableCell>{order.status!.title}</TableCell>
    </TableRow>
  )
}

export type {OrderRowProps}
export {OrderRow}
