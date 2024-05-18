import {Order} from "@models/*";
import {TableCell, TableRow} from "@mui/material";
import dayjs from "dayjs";

type OrderRowProps = {
  order: Order,
  onOrderSelect: () => void
}

function OrderRow(props: OrderRowProps) {
  const {order, onOrderSelect} = props

  return (
    <TableRow sx={{cursor: "pointer"}} onClick={() => {onOrderSelect()}}>
      <TableCell>{order.number}</TableCell>
      <TableCell>{order.totalPrice}</TableCell>
      <TableCell>{dayjs(order.dateTime).format(`DD-MM-YYYY HH:mm`)}</TableCell>
      <TableCell>{order.status!.title}</TableCell>
    </TableRow>
  )
}

export type {OrderRowProps}
export {OrderRow}
