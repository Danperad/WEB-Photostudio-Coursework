import {Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from "@mui/material";
import {Order} from "@models/*";
import {OrderRow} from "./OrderRow.tsx";

type ClientOrdersProps = {
  orders: Order[],
  onOrderSelect: (order: Order) => void
}

function ClientOrders(props: ClientOrdersProps) {
  const {orders, onOrderSelect} = props

  return (
    <TableContainer>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Номер</TableCell>
            <TableCell>Цена</TableCell>
            <TableCell>Дата и время</TableCell>
            <TableCell>Статус</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {orders.map(order => (
            <OrderRow key={order.number} order={order} onOrderSelect={() => {onOrderSelect(order)}}/>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  )
}

export type {ClientOrdersProps}
export {ClientOrders}
