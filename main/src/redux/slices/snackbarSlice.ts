import {createSlice, PayloadAction} from '@reduxjs/toolkit'

export interface SnackbarText {
  key: number,
  error: string
}

const snackbarSlice = createSlice({
  name: "Snackbar",
  initialState: {state: [] as SnackbarText[]},
  reducers: {
    ErrorAction: (state, action: PayloadAction<string>) => {
      state.state.push({error: action.payload, key: new Date().getTime() + Math.random()} as SnackbarText)
    },
    RemoveAction: (state, action: PayloadAction<number>) => {
      state.state = state.state.filter(s => s.key !== action.payload);
    }
  }
})

export const snackbarReducer = snackbarSlice.reducer;
export const snackbarActions = snackbarSlice.actions;
