import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import IconEcosystem from './IconEcosystem.vue'

describe('IconEcosystem', () => {
  it('renderiza un svg', () => {
    const wrapper = mount(IconEcosystem)
    expect(wrapper.find('svg').exists()).toBe(true)
  })
})
