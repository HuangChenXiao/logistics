import request from '@/utils/request'

export function getFrontStoreCategoryList(data) {
    return request({
        url: 'front/store/getFrontStoreCategoryList.doAdminJJ',
        method: "post",
        data: data
    })
}

export function getFrontStoreGoodsList(data) {
    return request({
        url: 'front/store/getFrontStoreGoodsList.doAdminJJ',
        method: "post",
        data: data
    })
}

export function getFrontCartList(data) {
    return request({
        url: 'front/cart/getFrontCartList.doAdminJJ',
        method: "post",
        data: data
    })
}
export function operationFrontCart(data) {
    return request({
        url: 'front/cart/operationFrontCart.doAdminJJ',
        method: "post",
        data: data
    })
}

export function operationFrontOrder(data) {
    return request({
        url: 'front/order/operationFrontOrder.doAdminJJ',
        method: "post",
        data: data
    })
}

export function getFrontStoreActivityList(data) {
    return request({
        url: 'front/store/getFrontStoreActivityList.doAdminJJ',
        method: "post",
        data: data
    })
}
export function operationFrontUserRedEnvelopes(data) {
    return request({
        url: 'front/user/operationFrontUserRedEnvelopes.doAdminJJ',
        method: "post",
        data: data
    })
}

export function operationFrontUserCollection(data) {
    return request({
        url: 'core/collection/operationFrontUserCollection.doAdminJJ',
        method: "post",
        data: data
    })
}

export function getFrontStoreList(data) {
    return request({
        url: 'front/store/getFrontStoreList.doAdminJJ',
        method: "post",
        data: data
    })
}

export function getFrontCartQuantity(data) {
    return request({
        url: 'front/cart/getFrontCartQuantity.doAdminJJ',
        method: "post",
        data: data
    })
}
export function getFrontUserEvaluateList(data) {
    return request({
        url: 'core/evaluate/getFrontUserEvaluateList.doAdminJJ',
        method: "post",
        data: data
    })
}