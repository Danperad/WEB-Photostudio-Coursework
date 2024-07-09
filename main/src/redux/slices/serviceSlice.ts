import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {Service} from "../../models/Models";

interface State {
  services: Service[],
  hasMore: boolean
}

const initState: State = {
  services: [],
  hasMore: true,
}

const serviceSlice = createSlice({
  name: 'services',
  initialState: initState,
  reducers: {
    ServicesLoaded: (state, action: PayloadAction<State>) => {
      action.payload.services.forEach((service) => {
        state.services.push(service);
      });
      state.hasMore = action.payload.hasMore;
    },
    ClearService: (state) => {
      state.services.splice(0, state.services.length);
      state.hasMore = true;
    }
  }
});

export const serviceReducer = serviceSlice.reducer;
export const serviceActions = serviceSlice.actions;
