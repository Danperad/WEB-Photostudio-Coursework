import {
  Button,
  Divider,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography
} from "@mui/material";
import {useEffect, useRef, useState} from "react";
import {EmployeeWithRole} from "@models/*";
import {getAllEmployees} from "../../services/employeeService.ts";

function Employees() {
  const [employees, setEmployees] = useState<EmployeeWithRole[]>([])
  const [selectedEmployee, setSelectedEmployee] = useState<EmployeeWithRole | undefined>(undefined)
  const key = useRef(false)
  useEffect(() => {
    if (key.current)
      return
    key.current = true
    getAllEmployees().then(res => {
      if (res.ok) {
        setEmployees(res.val)
      } else {
        console.log("ha")
      }
    })

  }, []);
  return (
    <Stack direction={"row"} width={"100%"}>
      <Stack width={"50%"}>
        <Stack direction={"row"} sx={{pl: 2, mt: 1}} spacing={2}>
          <Button variant={"contained"} onClick={() => {
            setSelectedEmployee(undefined)
          }}>
            Новый сотрудник
          </Button>
        </Stack>
        <TableContainer>
          <Table stickyHeader style={{width: '100%'}}>
            <TableHead>
              <TableRow sx={{cursor: "default"}}>
                <TableCell>Фамилия</TableCell>
                <TableCell>Имя</TableCell>
                <TableCell>Отчество</TableCell>
                <TableCell>Должность</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {employees.map(e => (
                <TableRow key={e.id} sx={{cursor: "pointer"}} onClick={() => {
                  setSelectedEmployee(e)
                }}>
                  <TableCell>{e.lastName}</TableCell>
                  <TableCell>{e.firstName}</TableCell>
                  <TableCell>{e.middleName}</TableCell>
                  <TableCell>{e.role.title}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Stack>
      <Divider orientation={"vertical"} variant={"middle"} flexItem/>
      {selectedEmployee && (
        <Stack width={"50%"} spacing={1} p={1}>
          <Typography>TODO:</Typography>
          <Typography>Редактирование сотрудников</Typography>
        </Stack>
      )}
    </Stack>
  )
}

export {Employees}
