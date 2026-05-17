import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import IconCommunity from './IconCommunity.vue'

describe('IconCommunity', () => {
  it('renderiza un svg', () => {
    const wrapper = mount(IconCommunity)
    expect(wrapper.find('svg').exists()).toBe(true)
  })
})
