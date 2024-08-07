import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {NewService, NewServicePackage} from "../../models/Models";

interface State {
  serviceModels: NewService[],
  servicePackage?: NewServicePackage,
  lastDate?: Date,
  lastDuration?: number
}

const cart = localStorage.getItem('cart');

const initState: State = cart === null ? {
  serviceModels: [],
} : JSON.parse(cart);

const cartSlice = createSlice({
  name: "cart",
  initialState: initState,
  reducers: {
    ServiceAdded: (state, action: PayloadAction<NewService>) => {
      state.serviceModels.push(action.payload);
      if (action.payload.startDateTime) {
        state.lastDate = action.payload.startDateTime;
        state.lastDuration = action.payload.duration;
      }
      localStorage.setItem('cart', JSON.stringify(state))
    },
    ServiceRemoved: (state, action: PayloadAction<NewService>) => {
      state.serviceModels = state.serviceModels.filter(s => s.id !== action.payload.id);
      localStorage.setItem('cart', JSON.stringify(state))
    },
    PackageAdded: (state, action: PayloadAction<NewServicePackage>) => {
      state.servicePackage = action.payload;
      localStorage.setItem('cart', JSON.stringify(state))
    },
    PackageRemoved: (state) => {
      state.servicePackage = undefined;
      localStorage.setItem('cart', JSON.stringify(state))
    },
    ClearCart: (state) => {
      state.servicePackage = undefined;
      state.serviceModels = [] as NewService[];
      state.lastDate = undefined;
      state.lastDuration = undefined;
      localStorage.removeItem('cart');
    }
  }
})

export const cartReducer = cartSlice.reducer;
export const cartActions = cartSlice.actions;
