import {ServiceWorker} from "./ServiceWorker.tsx";
import {Stack} from "@mui/material";
import Sidebar from "./components/Sidebar.tsx";
import {Route, Routes, useNavigate} from "react-router-dom";
import {Auth, Clients, Employees, EmployeeServices, Orders} from "./routes";
import {useSelector} from "react-redux";
import {RootState} from "./redux/store.ts";
import {AuthEmployee} from "@models/*";
import {useEffect} from "react";

function App() {
  const rootState = useSelector((state: RootState) => state.employee.employee) as (AuthEmployee | undefined);
  const navigate = useNavigate();

  useEffect(() => {
    if (!rootState)
      navigate("auth")
    else if (rootState.role > 2) {
      navigate("employeesServices")
    } else {
      navigate("clients")
    }
  }, [rootState]);
  return (
    <>
      <ServiceWorker/>
      <Routes>
        {!rootState && (
          <Route path={"auth"} element={<Auth/>}/>
        )}
        {rootState && (
          <Stack direction={"row"}>
            <Sidebar/>
            {rootState.role <= 2 && (
              <>
                <Route path={"clients"} element={<Clients/>}/>
                <Route path={"orders"} element={<Orders/>}/>
                <Route path={"employees"} element={<Employees/>}/>
              </>
            )}
            {rootState.role > 2 && (
              <Route path={"employeesServices"} element={<EmployeeServices/>}/>
            )}
          </Stack>
        )}
      </Routes>
    </>
  )
}

export default App;
