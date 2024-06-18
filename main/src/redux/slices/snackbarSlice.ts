import {createSlice, PayloadAction} from '@reduxjs/toolkit'

export interface SnackbarText {
    key: number,
    value: string,
    variant: 'success' | 'error'
}

const snackbarSlice = createSlice({
    name: "Snackbar",
    initialState: {state: [] as SnackbarText[]},
    reducers: {
        okAction: (state, action: PayloadAction<string>) => {
            state.state.push({
                value: action.payload,
                key: new Date().getTime() + Math.random(),
                variant: "success"
            } as SnackbarText)
        },
        ErrorAction: (state, action: PayloadAction<string>) => {
            state.state.push({
                value: action.payload,
                key: new Date().getTime() + Math.random(),
                variant: "error"
            } as SnackbarText)
        },
        RemoveAction: (state, action: PayloadAction<number>) => {
            state.state = state.state.filter(s => s.key !== action.payload);
        }
    }
})

export const snackbarReducer = snackbarSlice.reducer;
export const snackbarActions = snackbarSlice.actions;