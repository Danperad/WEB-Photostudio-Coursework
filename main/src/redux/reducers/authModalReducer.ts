import {createReducer} from "@reduxjs/toolkit";
import {CloseModal, OpenModal} from '../actions/authModalActions';

export const authModalReducer = createReducer(false, (builder) => {
  builder.addCase(OpenModal, () => {
    return true;
  }).addCase(CloseModal, () => {
    return false;
  })
})
