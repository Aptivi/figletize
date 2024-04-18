
# Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
# Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

OUTPUTS = Figletize.*/bin Figletize.*/obj Figletize/bin Figletize/obj

.PHONY: all

# General use

all: all-online

all-online:
	$(MAKE) -C tools invoke-build

dbg:
	$(MAKE) -C tools invoke-build ENVIRONMENT=Debug

doc:
	$(MAKE) -C tools invoke-doc-build

clean:
	rm -rf $(OUTPUTS)

# This makefile is just a wrapper for tools scripts.
