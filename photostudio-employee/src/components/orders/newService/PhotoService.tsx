import {NewServiceProps} from "./newServiceProps.ts";
import {
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
  Stack,
  Typography
} from "@mui/material";
import {useEffect, useState} from "react";
import {Employee, OrderService} from "@models/*";
import {getAvailableEmployees} from "../../../services/employeeService.ts";

function PhotoService(props: NewServiceProps) {
  const {service, startTime, duration,isAllowSelect, onComplete} = props
  const [employees, setEmployees] = useState<Employee[]>([])
  const [selectedEmployee, setSelectedEmployee] = useState<Employee | undefined>(undefined)

  useEffect(() => {
    if (!isAllowSelect())
      return
    getAvailableEmployees(startTime, duration, service.id).then(res => {
      if (res.ok){
      setEmployees(res.val)
      }
      else {
        console.log(res.val)
      }
    }).catch(err => console.log(err))
  }, [startTime, duration]);

  const handleSelectEmployee = (event: SelectChangeEvent) => {
    setSelectedEmployee(employees[employees.findIndex((val) => val.id === +event.target.value)])
  };

  const handleComplete = () => {
    const orderService: OrderService = {
      service: service.id,
      startDateTime: startTime,
      duration: duration,
      employee: selectedEmployee?.id
    }
    onComplete(orderService)
  }

  return (
    <Stack direction={"column"}>
      <Typography variant={"body2"}>{service.description}</Typography>
      <Stack direction={"row"}>
        <FormControl fullWidth>
          <InputLabel id="employee-select-label">Сотрудники</InputLabel>
          <Select value={selectedEmployee ? String(selectedEmployee.id) : ``} label={"Сотрудники"}
                  labelId={"employee-select-label"} onChange={handleSelectEmployee} disabled={!isAllowSelect()}>
            <MenuItem value={``}>
              <em>None</em>
            </MenuItem>
            {employees.map((employee) => (
              <MenuItem key={employee.id} value={employee.id}>{employee.lastName}</MenuItem>
            ))}
          </Select>
        </FormControl>
      </Stack>
      <Button variant={"contained"} onClick={handleComplete} disabled={!isAllowSelect() || !selectedEmployee}>Добавить</Button>
    </Stack>
  )
}

export {PhotoService}
