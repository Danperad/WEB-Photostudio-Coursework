import {NewServiceProps} from "./newServiceProps.ts";
import {Button, Stack, Typography} from "@mui/material";
import {useState} from "react";
import {OrderService} from "@models/*";

function SimpleService(props: NewServiceProps) {
  const {service, onComplete} = props
  const [orderService] = useState<OrderService>(
    {
      service: service
    }
  )
  return (
    <Stack spacing={1}>
      <Typography variant={"body2"}>{service.description}</Typography>
      <Button variant={"contained"} onClick={() => {onComplete(orderService)}}>Добавить</Button>
    </Stack>
  )
}

export {SimpleService}
