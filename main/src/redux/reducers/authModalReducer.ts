import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {CloseModal, OpenModal} from '../actions/authModalActions';

export const authModalReducer = createReducer(false, (builder) => {
    builder.addCase(OpenModal, (state, action: PayloadAction<boolean>) => {
        return true;
    }).addCase(CloseModal, (state, action: PayloadAction<boolean>) => {
        return false;
    })
})