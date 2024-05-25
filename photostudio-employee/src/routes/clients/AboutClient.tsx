import {useEffect, useRef, useState} from "react";
import {Client, Order} from "@models/*";
import {Button, IconButton, Stack, Typography} from "@mui/material";
import {Close} from '@mui/icons-material';
import {ClientOrders} from "../../components/orders/ClientOrders.tsx";
import {NewOrderTab} from "../../components/orders/NewOrderTab.tsx";
import {OrderInfo} from "../../components/orders/OrderInfo.tsx";
import {getOrdersByClient} from "../../services/orderService.ts";

type AboutClientProps = {
  client: Client
  close: () => void
}

enum TabStatus {
  Orders,
  OrderInfo,
  NewOrder
}

function AboutClient(props: AboutClientProps) {
  const {client, close} = props;
  const lastSelectedClient = useRef<number | undefined>(undefined)
  const [orders, setOrders] = useState<Order[]>([]);
  const [selectedOrder, setSelectedOrder] = useState<Order | undefined>(undefined)
  const [currentTabStatus, setTabStatus] = useState<TabStatus>(TabStatus.Orders)

  useEffect(() => {
    if (!client) {
      lastSelectedClient.current = undefined
      return
    }
    if (client.id === lastSelectedClient.current) {
      return;
    }
    lastSelectedClient.current = client.id
    searchOrders()
    setTabStatus(TabStatus.Orders)
  }, [client])

  const searchOrders = () => {

    getOrdersByClient(client).then(res => {
      if (res.ok){
        setOrders(res.val)
      }
      else {
        console.log(res.val)
      }
    }).catch(err => console.log(err))
  }

  const fullName = () => {
    let name = client.lastName + ` ` + client.firstName.charAt(0) + `.`
    if (client.middleName)
      name += ` ` + client.middleName.charAt(0) + `.`
    return name
  }

  const newOrderHandler = () => {
    setTabStatus(TabStatus.NewOrder)
  };

  const orderSelectHandler = (order: Order) => {
    setSelectedOrder(order)
    setTabStatus(TabStatus.OrderInfo)
  };

  const closeNewOrderHandler = () => {
    setTabStatus(TabStatus.Orders)
  };

  const closeOrderInfoHandler = () => {
    setSelectedOrder(undefined)
    setTabStatus(TabStatus.Orders)
  };

  const orderCreatedHandler = () => {
    searchOrders()
    closeNewOrderHandler()
  };

  const orderStatusChangedHandler = () => {
    searchOrders()
  };

  return (
    <>
      {currentTabStatus === TabStatus.Orders && (
        <Stack direction={"column"} width={'50%'}>
          <Stack direction={"row"}>
            <IconButton onClick={close}>
              <Close/>
            </IconButton>
            <Typography>{fullName()}</Typography>
            <Button variant={"contained"} onClick={newOrderHandler}>Новая заявка</Button>
          </Stack>
          <ClientOrders orders={orders} onOrderSelect={orderSelectHandler}/>
        </Stack>
      )}
      {currentTabStatus === TabStatus.NewOrder && (
        <NewOrderTab client={client} close={closeNewOrderHandler} onOrderCreated={orderCreatedHandler}/>
      )}
      {currentTabStatus === TabStatus.OrderInfo && selectedOrder && (
        <OrderInfo order={selectedOrder} close={closeOrderInfoHandler} onOrderStatusChanged={orderStatusChangedHandler}/>
      )}
    </>
  )
}

export type {AboutClientProps};
export {AboutClient};
