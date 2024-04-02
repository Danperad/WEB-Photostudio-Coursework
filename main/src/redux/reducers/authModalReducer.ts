import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {CloseModal, OpenModal} from '../actions/authModalActions';

export const authModalReducer = createReducer(false, (builder) => {
  builder.addCase(OpenModal, (_, __: PayloadAction<boolean>) => {
    return true;
  }).addCase(CloseModal, (_, __: PayloadAction<boolean>) => {
    return false;
  })
})
