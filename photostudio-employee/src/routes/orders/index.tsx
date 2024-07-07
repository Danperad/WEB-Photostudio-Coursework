import {useEffect, useState} from "react";
import {Button, Divider, Stack, TextField} from "@mui/material";
import {useNavigate} from "react-router-dom";
import {OrdersTable} from "./OrdersTable.tsx";
import {Order} from "@models/*";
import {OrderInfo} from "../../components/orders/OrderInfo.tsx";
import {getAllOrders} from "../../services/orderService.ts";
import {Reports} from "./Reports.tsx";

function Orders() {
  const [search, setSearch] = useState<string>(``)
  const [orders, setOrders] = useState<Order[]>([]);
  const [isReportsOpen, setIsReportsOpen] = useState<boolean>(false)
  const [selectedOrder, setSelectedOrder] = useState<Order | undefined>(undefined)

  const navigate = useNavigate();

  useEffect(() => {
    const timer = setTimeout(() => {
      searchOrders()
    }, 200)
    return () => clearTimeout(timer)
  }, [search])

  useEffect(() => {
    if (selectedOrder) {
      setSelectedOrder(prevOrder => orders[orders.findIndex(order => order.number === prevOrder?.number)])
    }
  }, [orders]);

  const searchOrders = () => {
    getAllOrders(search ? search : undefined).then(res => {
      if (res.ok) {
        setOrders(res.val)
      } else {
        console.log(res.val)
      }
    })
  }

  useEffect(() => {
    if (isReportsOpen)
      setSelectedOrder(undefined)
  }, [isReportsOpen]);

  const handelNewOrderClick = () => {
    navigate("/clients")
  };

  const handelOrderSelect = (order: Order) => {
    setSelectedOrder(order)
    setIsReportsOpen(false)
  }

  const handleOrderInfoClose = () => {
    setSelectedOrder(undefined)
  }

  const orderStatusChangedHandler = () => {
    searchOrders()
  };

  return (
    <Stack direction={"row"} width={"100%"}>
      <Stack direction={"column"} width={"50%"}>
        <Stack direction={"row"} sx={{pl: 2, mt: 1}} spacing={2}>
          <TextField label={"Поиск"} size={"small"} value={search} onChange={e => {
            setSearch(e.target.value)
          }}></TextField>
          <Button variant={"contained"} onClick={handelNewOrderClick}>
            Новая заявка
          </Button>
          <Button variant={"contained"} onClick={() => setIsReportsOpen(true)}>
            Генерация отчетов Отчеты
          </Button>
        </Stack>
        <OrdersTable orders={orders} onOrderSelect={handelOrderSelect}/>
      </Stack>
      <Divider orientation={"vertical"} variant={"middle"} flexItem/>
      <Stack direction={"column"} width={"50%"}>
        {selectedOrder && (
          <OrderInfo order={selectedOrder} close={handleOrderInfoClose}
                     onOrderStatusChanged={orderStatusChangedHandler}/>
        )}
        {isReportsOpen && (
          <Reports/>
        )}
      </Stack>
    </Stack>
  )
}

export {Orders}
