import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import Sidebar from "./components/Sidebar.tsx";
import {Stack} from "@mui/material";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Auth, Clients, Employees, EmployeeServices, Orders, Services} from "./routes";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDayjs} from '@mui/x-date-pickers/AdapterDayjs'
import {ServiceWorker} from "./ServiceWorker.tsx";


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <BrowserRouter>
        <ServiceWorker/>
        <Stack direction={"row"}>
          <Sidebar/>
          <Routes>
            <Route path={"auth"} element={<Auth/>}/>
            <Route path={"clients"} element={<Clients/>}/>
            <Route path={"orders"} element={<Orders/>}/>
            <Route path={"services"} element={<Services/>}/>
            <Route path={"employees"} element={<Employees/>}/>
            <Route path={"employeesServices"} element={<EmployeeServices/>}/>
          </Routes>
        </Stack>
      </BrowserRouter>
    </LocalizationProvider>
  </React.StrictMode>,
)
