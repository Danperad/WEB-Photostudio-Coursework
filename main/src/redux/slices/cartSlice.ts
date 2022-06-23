import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {NewServiceModel} from "../../models/Models";

interface State {
    serviceModels: NewServiceModel[],
    servicePackage: null
}

const initState : State = {
    serviceModels: [],
    servicePackage: null
};

const cartSlice = createSlice({
    name: "cart",
    initialState: initState,
    reducers:{
        ServiceAdded: (state, action: PayloadAction<NewServiceModel>) => {
            state.serviceModels.push(action.payload);
        },
        ServiceRemoved: (state, action: PayloadAction<NewServiceModel>) => {
            state.serviceModels = state.serviceModels.filter(s => s===action.payload)
        }
    }
})

export const cartReducer = cartSlice.reducer;
export const cartActions = cartSlice.actions;