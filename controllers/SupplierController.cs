[HttpPost]
public async Task<IActionResult> Create([FromBody] Supplier supplier)
{
    var created = await _service.CreateAsync(supplier);
    return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
}

[HttpGet("{id}")]
public async Task<IActionResult> GetById(Guid id)
{
    var sup = await _service.GetByIdAsync(id);
    return sup is not null ? Ok(sup) : NotFound();
}
