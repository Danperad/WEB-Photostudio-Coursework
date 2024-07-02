import {AuthEmployee} from "@models/*";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";

export interface EmployeeState {
  employee: AuthEmployee | undefined
}

const initState: EmployeeState = {
  employee: undefined
}

const employeeSlice = createSlice({
  name: "employee",
  initialState: initState,
  reducers: {
    loginSuccess: (state, action: PayloadAction<AuthEmployee>) => {
      state.employee = action.payload;
    },
    logout: (state) => {
      state.employee = undefined;
    }
  }
})

export const employeeReducer = employeeSlice.reducer;
export const employeeActions = employeeSlice.actions;
