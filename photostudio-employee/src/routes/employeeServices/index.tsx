import {useEffect, useState} from "react";
import {Divider, FormControlLabel, FormGroup, Paper, Stack, Switch, Typography} from "@mui/material";
import {OrderServiceInfo} from "./OrderServiceInfo.tsx";
import {EmployeeService, Service, Status} from "@models/*";
import {getServicesByEmployee} from "../../services/employeeService.ts";

function EmployeeServices() {
  const [isShowClosed, setShowClosed] = useState<boolean>(false)
  const [services, setServices] = useState<EmployeeService[]>([])
  const [selectedService, setSelectedService] = useState<EmployeeService | undefined>()

  useEffect(() => {
    searchServices()
  }, [isShowClosed])

  useEffect(() => {
    if (selectedService)
      setSelectedService(prevState => {
        const serv = services.findIndex(s => s.id === prevState?.id)
        if (serv === -1) return undefined
        return services[serv]
      })
  }, [services]);

  const searchServices = () => {
    getServicesByEmployee(4, isShowClosed).then(res => {
      if (res.ok) {
        setServices(res.val)
      } else {
        console.log(res.val)
      }
    })
  }

  const handleShowClosedChange = () => {
    setShowClosed(prevState => !prevState)
  }

  const handleServiceSelected = (service: EmployeeService) => {
    setSelectedService(service)
  }

  const handleServiceInfoClose = () => {
    setSelectedService(undefined)
  }

  const handleStatusChange = () => {
    searchServices()
  }

  return (
    <Stack direction={"row"} width={"100%"}>
      <Stack direction={"column"} width={"50%"} spacing={1} px={1}>
        <FormGroup>
          <FormControlLabel control={<Switch checked={isShowClosed} onChange={handleShowClosedChange}/>}
                            label="Показывать завершенные"/>
        </FormGroup>
        {services.map(service => (
          <Paper key={service.id} sx={{p: 1, cursor: "pointer"}} elevation={1} onClick={() => {
            handleServiceSelected(service)
          }}>
            <Typography>{(service.service as Service).title},
              Статус: <em>{(service.status as Status).title}</em></Typography>
          </Paper>
        ))}
      </Stack>
      <Divider orientation={"vertical"} variant={"middle"} flexItem/>
      <Stack direction={"column"} width={"50%"}>
        {selectedService && (
          <OrderServiceInfo orderService={selectedService} onClose={handleServiceInfoClose}
                            onOrderServiceStatusChange={handleStatusChange}/>
        )}
      </Stack>
    </Stack>
  )
}

export {EmployeeServices}
