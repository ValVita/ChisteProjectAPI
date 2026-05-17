import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import IconTooling from './IconTooling.vue'

describe('IconTooling', () => {
  it('renderiza un svg', () => {
    const wrapper = mount(IconTooling)
    expect(wrapper.find('svg').exists()).toBe(true)
  })
})
