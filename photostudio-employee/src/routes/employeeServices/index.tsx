import {useEffect, useState} from "react";
import {Divider, FormControlLabel, FormGroup, Paper, Stack, Switch, Typography} from "@mui/material";
import {OrderServiceInfo} from "./OrderServiceInfo.tsx";
import {EmployeeService, Service} from "@models/*";
import {getServicesByEmployee} from "../../services/employeeService.ts";

function EmployeeServices() {
  const [isShowClosed, setShowClosed] = useState<boolean>(false)
  const [services, setServices] = useState<EmployeeService[]>([])
  const [selectedService, setSelectedService] = useState<EmployeeService | undefined>()

  useEffect(() => {
    getServicesByEmployee(2, isShowClosed).then(res => {
      if (res.ok){
        setServices(res.val)
      }
      else {
        console.log(res.val)
      }
    })
  }, [isShowClosed])


  const handleShowClosedChange = () => {
    setShowClosed(prevState => !prevState)
  }

  const handleServiceSelected = (service: EmployeeService) => {
    setSelectedService(service)
  }

  const handleServiceInfoClose = () => {
    setSelectedService(undefined)
  }

  return (
    <Stack direction={"row"} width={"100%"}>
      <Stack direction={"column"} width={"50%"}>
        <FormGroup>
          <FormControlLabel control={<Switch checked={isShowClosed} onChange={handleShowClosedChange}/>}
                            label="Показывать завершенные"/>
        </FormGroup>
        {services.map(service => (
          <Paper key={service.id} elevation={2} onClick={() => {
            handleServiceSelected(service)
          }}>
            <Typography>{(service.service as Service).title}</Typography>
          </Paper>
        ))}
      </Stack>
      <Divider orientation={"vertical"} variant={"middle"} flexItem/>
      <Stack direction={"column"} width={"50%"}>
        {selectedService && (
          <OrderServiceInfo orderService={selectedService} onClose={handleServiceInfoClose}/>
        )}
      </Stack>
    </Stack>
  )
}

export {EmployeeServices}
