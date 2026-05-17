import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { mount, flushPromises } from '@vue/test-utils'
import HelloWorld from './HelloWorld.vue'

describe('HelloWorld', () => {
  beforeEach(() => {
    vi.stubGlobal('fetch', vi.fn())
  })

  afterEach(() => {
    vi.unstubAllGlobals()
  })

  it('muestra estado de carga inicialmente', () => {
    vi.mocked(fetch).mockReturnValue(new Promise(() => {}))

    const wrapper = mount(HelloWorld)

    expect(wrapper.text()).toContain('Cargando datos...')
  })

  it('muestra la tabla cuando la petición es exitosa', async () => {
    const jokes = [
      {
        id: 1,
        externalId: 'ext-1',
        content: 'Chiste uno',
        createdAt: '2025-01-01T00:00:00Z',
      },
    ]
    vi.mocked(fetch).mockResolvedValue({
      ok: true,
      json: async () => jokes,
    })

    const wrapper = mount(HelloWorld)
    await flushPromises()

    expect(wrapper.text()).toContain('Mis datos desde SQL Server')
    expect(wrapper.text()).toContain('ext-1')
    expect(wrapper.text()).toContain('Chiste uno')
    expect(fetch).toHaveBeenCalledWith('/api/Jokes')
  })

  it('muestra error cuando la petición falla', async () => {
    vi.mocked(fetch).mockResolvedValue({
      ok: false,
      status: 500,
    })

    const wrapper = mount(HelloWorld)
    await flushPromises()

    expect(wrapper.text()).toContain('No se pudieron cargar los datos')
  })

  it('muestra error cuando fetch lanza excepción', async () => {
    vi.mocked(fetch).mockRejectedValue(new Error('Network error'))

    const wrapper = mount(HelloWorld)
    await flushPromises()

    expect(wrapper.text()).toContain('No se pudieron cargar los datos')
  })
})
