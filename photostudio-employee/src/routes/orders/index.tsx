import {useEffect, useState} from "react";
import {Button, Divider, Stack, TextField, Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";
import {OrdersTable} from "./OrdersTable.tsx";
import {Order} from "@models/*";
import {OrderInfo} from "../../components/orders/OrderInfo.tsx";
import {getAllOrders} from "../../services/orderService.ts";

function Orders() {
  const [search, setSearch] = useState<string>(``)
  const [orders, setOrders] = useState<Order[]>([]);
  const [selectedOrder, setSelectedOrder] = useState<Order | undefined>(undefined)

  const navigate = useNavigate();

  useEffect(() => {
    setTimeout(() => {
      getAllOrders(search ? search : undefined).then(res => {
        if (res.ok){
          setOrders(res.val)
        }
        else {
          console.log(res.val)
        }
      })
    }, 200)
  }, [search])

  const handelNewOrderClick = () => {
    navigate("/clients")
  };

  const handelOrderSelect = (order: Order) => {
    setSelectedOrder(order)
  }

  const handleOrderInfoClose = () => {
    setSelectedOrder(undefined)
  }

  return (
    <Stack direction={"row"} width={"100%"}>
      <div className={"hidden"}>
        <Typography>TODO:</Typography>
        <Typography>Список всех заявок</Typography>
        <Typography>Поиск</Typography>
        <Typography>Просмотр</Typography>
      </div>
      <Stack direction={"column"} width={"50%"}>
        <Stack direction={"row"}>
          <TextField label={"Поиск"} size={"small"} value={search} onChange={e => {
            setSearch(e.target.value)
          }}></TextField>
          <Button variant={"contained"} onClick={handelNewOrderClick}>
            Новая заявка
          </Button>
        </Stack>
        <OrdersTable orders={orders} onOrderSelect={handelOrderSelect}/>
      </Stack>
      <Divider orientation={"vertical"} variant={"middle"} flexItem/>
      <Stack direction={"column"} width={"50%"}>
        {selectedOrder && (
          <OrderInfo order={selectedOrder} close={handleOrderInfoClose}/>
        )}
      </Stack>
    </Stack>
  )
}

export {Orders}
