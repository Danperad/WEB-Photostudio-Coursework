import {Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";
import {OrderRow} from "./OrderRow.tsx";
import {Order} from "@models/*";

type OrdersTableProps = {
  orders: Order[]
  onOrderSelect: (order: Order) => void
}

function OrdersTable(props: OrdersTableProps) {
  const {orders, onOrderSelect} = props
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Номер заявки</TableCell>
          <TableCell>Клиент</TableCell>
          <TableCell>Дата и время</TableCell>
          <TableCell>Стоимость</TableCell>
          <TableCell>Статус</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {orders.map(order => (
          <OrderRow key={order.number} order={order} onOrderSelect={() => {
            onOrderSelect(order)
          }}/>
        ))}
      </TableBody>
    </Table>
  )
}

export {OrdersTable}
